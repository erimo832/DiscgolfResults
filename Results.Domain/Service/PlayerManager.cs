using Results.Domain.Configuration.External;
using Results.Domain.Model;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class PlayerManager : IPlayerManager
    {
        private IPlayerRepository Repository { get; }
        private IPlayerCourseLayoutHcpRepository PlayerCourseLayoutHcpRepository { get; }

        public PlayerManager(IPlayerRepository playerRepository, IPlayerCourseLayoutHcpRepository playerCourseLayoutHcpRepository)
        {
            Repository = playerRepository;
            PlayerCourseLayoutHcpRepository = playerCourseLayoutHcpRepository;
        }

        public Player GetPlayer(string pdgaNr, string firstName, string lastName, IDuplicatePlayerConfiguration duplicatePlayers)
        {
            pdgaNr = pdgaNr.Trim();
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (IsDuplicate(pdgaNr, firstName, lastName, duplicatePlayers, out var replacePlayer))
            {
                pdgaNr = replacePlayer.PdgaNumberAsString;
                firstName = replacePlayer.FirstName;
                lastName = replacePlayer.LastName;
            }

            var players = Repository.GetAll();

            var player = players.FirstOrDefault(x => x.PdgaNumber != null && x.PdgaNumber.ToString() == pdgaNr);

            if (player == null)
            {
                player = players.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);

                if (player != null && pdgaNr != "" && player.PdgaNumber == null)
                {
                    player.PdgaNumber = pdgaNr == "" ? null : Convert.ToInt32(pdgaNr);
                    Repository.Update(player);
                }
            }

            if (player == null)
            {
                player = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PdgaNumber = pdgaNr == "" ? null : Convert.ToInt32(pdgaNr),
                };

                Repository.Insert(new List<Player>
                {
                    player
                });
            }

            return player;
        }

        private bool IsDuplicate(string pdgaNr, string firstName, string lastName, IDuplicatePlayerConfiguration duplicate, out Player replacePlayer)
        {
            replacePlayer = new Player();

            foreach (var player in duplicate.DuplicatePlayers)
            {
                if (player.PlayerAliases.Any(x => x.FirstName == firstName && x.LastName == lastName)) //&& (pdgaNr == "" || x.PdgaNumber == pdgaNr)
                {
                    var primary = player.PlayerAliases.FirstOrDefault(x => x.IsPrimary);

                    if (primary == null)
                        throw new ArgumentException("Invalid DuplicatePlayerConfiguration");

                    if (pdgaNr == primary.PdgaNumberAsString && firstName == primary.FirstName && lastName == primary.LastName)
                        return false;

                    replacePlayer.FirstName = primary.FirstName;
                    replacePlayer.LastName = primary.LastName;
                    replacePlayer.PdgaNumber = primary.PdgaNumber;

                    return true;
                }
            }

            return false;
        }

        public IList<Player> GetAll()
        {
            return Repository.GetAll();
        }

        public Player Get(int playerId)
        {
            return Repository.Get(playerId);
        }

        public void InsertPlayerHcps(IList<PlayerCourseLayoutHcp> layoutHcps)
        {
            PlayerCourseLayoutHcpRepository.Insert(layoutHcps);
        }
    }
}

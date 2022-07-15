import i18n from "i18next";
import { initReactI18next } from "react-i18next";

// TODO: Move translations it its own json files
const resources = {
  en: {
    translation: {       
      "language": "English",           
      "common_loading": "Loading...",
      "column_date": "Date",
      "column_round": "Round",
      "column_name": "Name",
      "column_hcp": "Hcp",
      "column_hcpafter": "New hcp",
      "column_hcpscore": "Hcp score",
      "column_score": "Score",
      "column_place": "Place",
      "column_points": "Points",
      "column_avgpoints": "Avgerage points",
      "column_totalpoints": "Total points",            
      "column_avgscore": "Average score",
      "column_totalscore": "Total score",
      "column_avghcpscore": "Average hcp score",
      "column_totalhcpscore": "Total hcp score",
      "column_rounds": "Rounds",
      "column_maxpoints": "Max points",
      "column_minpoints": "Min points",
      "column_maxthrows": "Max throws",
      "column_minthrows": "Min throws",
      "column_ctp": "Ctp",
      "column_numerofctps": "Number of ctp",
      "menu_home": "Home",
      "menu_hcp": "Hcp",
      "menu_players": "Players",
      "menu_rounds": "Rounds",
      "menu_leaderboards": "Leaderboards",
      "menu_scoreleaderboard": "Score",
      "menu_pointleaderboard": "Points",
      "menu_ctpleaderboard": "Ctp",
      "home_header": "",
      "home_description": "",
      "hcp_header": "Current handicap",
      "hcp_description_avgscore": "Your last 18 rounds and the best 1/3 of the rounds is used to calculate average score. (If you have 4 rounds. It should be based on 1.3333.. rounds. But it is rounded up it is based on 2.)",      
      "hcp_description_hcp": "Hcp = (Average score  - 48) * 0.8.",
      "players_header": "Player statistics",
      "players_description": "Click on player in table to see more information",
      "playersinfo_no_found": "Player not found",
      "rounds_header": "Rounds",
      "rounds_description": "The lastest rounds with results.",
      "series_header": "Series",
      "series_description": "Series with results.",
      "series_basednumrounds": "Based on the {{cnt}} best point rounds for each players.",
      "leaderboard_points_header": "Points leaderboard",
      "leaderboard_score_header": "Score leaderboard",
      "leaderboard_ctp_header": "Ctp leaderboard",
    }
  },
  sv: {
    translation: { 
      "language": "Svenska",
      "common_loading": "Laddar...",
      "column_date": "Datum",
      "column_round": "Runda",
      "column_name": "Namn",
      "column_hcp": "Hcp",
      "column_hcpafter": "Nytt hcp",
      "column_hcpscore": "Kast hcp",
      "column_score": "Kast",
      "column_place": "Plats",
      "column_points": "Poäng",
      "column_avgpoints": "Snittpoäng",
      "column_totalpoints": "Totalpoäng",            
      "column_avgscore": "Snittkast",
      "column_totalscore": "Totalkast",
      "column_avghcpscore": "Snittkast hcp",
      "column_totalhcpscore": "Totalkast hcp",
      "column_rounds": "Rundor",
      "column_maxpoints": "Max poäng",
      "column_minpoints": "Min poäng",
      "column_maxthrows": "Maxkast",
      "column_minthrows": "Minkast",      
      "column_ctp": "Ctp",
      "column_numerofctps": "Antal ctp",
      "menu_home": "Hem",
      "menu_hcp": "Hcp",
      "menu_players": "Spelare",
      "menu_rounds": "Rundor",
      "menu_leaderboards": "Tabeller",
      "menu_scoreleaderboard": "Kast",
      "menu_pointleaderboard": "Poäng",
      "menu_ctpleaderboard": "Ctp",
      "home_header": "",
      "home_description": "",
      "hcp_header": "Nuvarande handicap",
      "hcp_description_avgscore": "Av dina senaste 18 rundor, så tas den bästa 1/3 av rundorna för att beräkna ett snitt av antalet kast. (Men om du bara har 4 rundor. Så görs en avrundning uppåt till att baseras på 2 rundor istället för 1.3333... rundor)",      
      "hcp_description_hcp": "Hcp = (Snitt antal kast  - 48) * 0.8.",
      "players_header": "Spelar statistik",
      "players_description": "Klicka på spelare i tabellen för att få mer information.",
      "playersinfo_no_found": "Spelare inte funnen",
      "rounds_header": "Rundor",
      "rounds_description": "Senaste rundorna med resultat.",
      "series_header": "Serier",
      "series_description": "Serier med results.",
      "series_basednumrounds": "Summeras på de {{cnt}} bästa poängrundorna för varje spelare.",
      "leaderboard_points_header": "Poängtabell",
      "leaderboard_score_header": "Kasttabell",
      "leaderboard_ctp_header": "Ctp-tabell",
    }
  }
};

i18n
  .use(initReactI18next) // passes i18n down to react-i18next
  .init({
    resources,
    lng: "sv",

    keySeparator: false, // we do not use keys in form messages.welcome

    interpolation: {
      escapeValue: false // react already safes from xss
    }
  }); 

  export default i18n;
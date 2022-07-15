import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Hcp } from './components/Hcp';
import { Rounds } from './components/Rounds';
import { PointLeaderboard } from './components/leaderboards/PointLeaderboard';
import { ScoreLeaderboard} from './components/leaderboards/ScoreLeaderboard';
import { CtpLeaderboard} from './components/leaderboards/CtpLeaderboard';
import { Players } from './components/players/Players';
import { Details } from './components/players/Details';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/hcp',
    element: <Hcp />
  },
  {
    path: '/rounds',
    element: <Rounds />
  },
  {
    path: '/players',
    element: <Players />
  },
  {
    path: '/players/:playerId',
    element: <Details />
  },
  {
    path: '/leaderboards/point',
    element: <PointLeaderboard />
  },
  {
    path: '/leaderboards/score',
    element: <ScoreLeaderboard />
  },
  {
    path: '/leaderboards/ctp',
    element: <CtpLeaderboard />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;

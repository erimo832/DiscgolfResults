import { Home } from "./components/Home";
import { Hcp } from './components/Hcp';
import { Rounds } from "./components/rounds/Rounds";
import { PointLeaderboard } from './components/leaderboards/PointLeaderboard';
import { ScoreLeaderboard} from './components/leaderboards/ScoreLeaderboard';
import { CtpLeaderboard} from './components/leaderboards/CtpLeaderboard';
import { Players } from './components/players/Players';
import { PlayerDetails } from './components/players/PlayerDetails';
import { Courses } from "./components/courses/Courses";
import { CourseDetails } from "./components/courses/CourseDetails";

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
    element: <PlayerDetails />
  },
  {
    path: '/courses',
    element: <Courses />
  },
  {
    path: '/courses/:courseId',
    element: <CourseDetails />
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
  }
];

export default AppRoutes;

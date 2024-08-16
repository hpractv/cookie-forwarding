import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Home } from './components/Home';
import { CookieForward } from './components/CookieForward';

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: '/counter',
    element: <Counter />,
  },
  {
    path: '/fetch-data',
    element: <FetchData />,
  },
  {
    path: '/cookie-forward',
    element: <CookieForward />,
  },
];

export default AppRoutes;

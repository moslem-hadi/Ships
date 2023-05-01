import {
  Routes,
  Route,
  Navigate,
  useNavigate,
  useLocation,
} from 'react-router-dom';

import { history } from './_helpers';
import { Nav, PrivateRoute, Modal } from './components';
import { Home } from './home';
import { Login } from './login';
import { useSelector } from 'react-redux';
import { ViewPage } from './view/ViewPage';

export { App };

function App() {
  // init custom history object to allow navigation from
  // anywhere in the react app (inside or outside components)
  history.navigate = useNavigate();
  history.location = useLocation();
  const { isOpen } = useSelector(store => store.modal);
  return (
    <>
      <div className={`app-container bg-light ${isOpen && 'model-opened'}`}>
        <Nav />
        <div className="container pt-4 pb-4">
          <Routes>
            <Route
              path="/"
              element={
                <PrivateRoute>
                  <Home />
                </PrivateRoute>
              }
            />{' '}
            <Route
              path="/view/:id"
              element={
                <PrivateRoute>
                  <ViewPage />
                </PrivateRoute>
              }
            />
            <Route path="/login" element={<Login />} />
            <Route path="*" element={<Navigate to="/" />} />
          </Routes>
        </div>
      </div>
      {isOpen && <Modal />}
    </>
  );
}

import { NavLink } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';

import { authActions } from '../_store';

export { Nav };

function Nav() {
  const authUser = useSelector(x => x.auth.user);
  const dispatch = useDispatch();
  const logout = () => dispatch(authActions.logout());

  // only show nav when logged in
  if (!authUser) return null;

  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div className="container-fluid">
          <a
            className="navbar-brand"
            asp-area=""
            asp-controller="Home"
            asp-action="Index"
          >
            <img src="/images/ship-svgrepo-com.svg" alt="" width="30" /> Ships
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target=".navbar-collapse"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
            <ul className="navbar-nav flex-grow-1">
              <li className="nav-item">
                <NavLink to="/" className="nav-item nav-link text-dark">
                  Home
                </NavLink>
              </li>
              <li className="nav-item">
                <button
                  onClick={logout}
                  className="btn btn-link nav-item nav-link"
                >
                  Logout
                </button>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
}

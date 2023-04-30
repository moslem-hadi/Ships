import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import Pagination from 'react-js-pagination';
import { shipsActions } from '../_store';

export { Home };

function Home() {
  const dispatch = useDispatch();
  const { user: authUser } = useSelector(x => x.auth);
  const { ships } = useSelector(x => x.ships);
  const pageSize = 2;

  const handlePageChange = async page => {
    await getShips(page);
  };
  useEffect(() => {
    getShips(1);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getShips = async (page = 1) => {
    await dispatch(shipsActions.getAll({ page, pageSize }));
  };
  return (
    <div>
      <h1>Hi {authUser?.firstName}!</h1>
      <p>You're logged in with React 18 + Redux & JWT!!</p>
      <h3>ships from secure api end point:</h3>
      {ships?.items?.length && (
        <>
          {' '}
          <ul>
            {ships.items.map((item, i) => (
              <li key={i}>
                {item.name} {item.shipCode.code}
              </li>
            ))}
          </ul>
          <Pagination
            activePage={ships.pageNumber}
            itemsCountPerPage={ships.itemsPerPage}
            totalItemsCount={ships.totalCount}
            pageRangeDisplayed={5}
            onChange={handlePageChange}
            itemClass="page-item"
            linkClass="page-link"
          />
        </>
      )}
      {ships?.loading && (
        <div className="spinner-border spinner-border-sm"></div>
      )}
      {ships?.error && (
        <div className="text-danger">
          Error loading ships: {ships.error.message}
        </div>
      )}
    </div>
  );
}

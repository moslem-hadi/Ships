import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import Pagination from 'react-js-pagination';
import { shipsActions } from '../_store';

export { Home };

function Home() {
  const dispatch = useDispatch();
  const { user: authUser } = useSelector(x => x.auth);
  const { ships } = useSelector(x => x.ships);
  const [pagerInfo, setPagerInfo] = useState({
    itemsPerPage: 2,
    pageNumber: 1,
    totalCount: 10,
  });

  const handlePageChange = async pageNumber => {
    console.log(`active page is ${pageNumber}`);
    await getShips(pageNumber);
    setPagerInfo(prevState => ({
      ...prevState,
      [pageNumber]: pageNumber,
    }));
  };
  useEffect(() => {
    getShips();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getShips = async (pageNumber = 1, pageSize = 2) => {
    await dispatch(shipsActions.getAll(pageNumber, pageSize));

    // setTimeout(() => {
    //   debugger;
    //   if (ships) {
    //     setPagerInfo({
    //       itemsPerPage: ships.itemsPerPage,
    //       pageNumber: ships.pageNumber,
    //       totalCount: ships.totalCount,
    //     });
    //   }
    // }, 1000);
  };
  return (
    <div>
      <h1>Hi {authUser?.firstName}!</h1>
      <p>You're logged in with React 18 + Redux & JWT!!</p>
      <h3>ships from secure api end point:</h3>
      {ships?.items?.length && (
        <ul>
          {ships.items.map((item, i) => (
            <li key={i}>
              {item.name} {item.shipCode.code}
            </li>
          ))}
        </ul>
      )}
      <Pagination
        activePage={pagerInfo.pageNumber}
        itemsCountPerPage={pagerInfo.itemsPerPage}
        totalItemsCount={pagerInfo.totalCount}
        pageRangeDisplayed={5}
        onChange={handlePageChange}
        itemClass="page-item"
        linkClass="page-link"
      />
      {ships.loading && (
        <div className="spinner-border spinner-border-sm"></div>
      )}
      {ships.error && (
        <div className="text-danger">
          Error loading ships: {ships.error.message}
        </div>
      )}
    </div>
  );
}

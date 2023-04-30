import Pagination from 'react-js-pagination';

function Ships({ ships, handlePageChange }) {
  return (
    <>
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
  );
}

export { Ships };

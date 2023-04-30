import Pagination from 'react-js-pagination';

function Ships({ ships, handlePageChange }) {
  return (
    <>
      <div class="table-responsive">
        <table class="table mb-0">
          <thead class="small text-uppercase bg-body text-muted">
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Code</th>
              <th>Length</th>
              <th>Width</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {ships.items.map((item, i) => (
              <tr class="align-middle" key={i}>
                <td>{item.id}</td>
                <td> {item.name}</td>
                <td>{item.shipCode.code}</td>
                <td> {item.length}</td>
                <td> {item.width}</td>

                <td>
                  <span>Details</span>
                  <span>Edit</span>
                  <span className="text-danger">Delete</span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div class="card-footer text-end">
        <Pagination
          activePage={ships.pageNumber}
          itemsCountPerPage={ships.itemsPerPage}
          totalItemsCount={ships.totalCount}
          pageRangeDisplayed={5}
          onChange={handlePageChange}
          itemClass="page-item"
          linkClass="page-link"
        />
      </div>
    </>
  );
}

export { Ships };

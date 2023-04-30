import Pagination from 'react-js-pagination';
import { Delete, Edit, Eye } from '../_helpers/isons';
function Ships({ ships, handlePageChange }) {
  return (
    <>
      <div className="table-responsive">
        <table className="table mb-0">
          <thead className="small text-uppercase bg-body text-muted">
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
              <tr className="align-middle" key={i}>
                <td>{item.id}</td>
                <td> {item.name}</td>
                <td>{item.shipCode.code}</td>
                <td> {item.length}</td>
                <td> {item.width}</td>

                <td width={120}>
                  <span className="icon me-3">
                    <Eye />
                  </span>
                  <span className="icon me-3">
                    <Edit />
                  </span>
                  <span className="text-danger icon">
                    <Delete />
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="card-footer text-end">
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

import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { shipsActions } from '../_store';
import { Ships, Loading } from '../components';

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
      <div class="row">
        <div class="col-12 mb-3 mb-lg-5">
          <div class="position-relative card table-nowrap table-card">
            <div class="card-header d-flex align-items-center justify-content-between">
              <h5 class="mb-0">Ships</h5>
              <button type="button" className="btn btn-outline-secondary">
                Add new
              </button>
            </div>
            {ships?.items?.length && (
              <Ships ships={ships} handlePageChange={handlePageChange} />
            )}
          </div>
        </div>
      </div>

      {ships?.loading && <Loading />}
      {ships?.error && (
        <div className="text-danger">
          Error loading ships: {ships.error.message}
        </div>
      )}
    </div>
  );
}

import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { shipsActions, modalActions, loadingActions } from '../_store';
import { Ships, Empty } from '../components';

export { Home };

function Home() {
  const dispatch = useDispatch();
  const { ships } = useSelector(x => x.ships);

  const handlePageChange = async page => {
    await getShips(page);
  };
  useEffect(() => {
    getShips(1);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getShips = async (page = 1) => {
    dispatch(loadingActions.startLoading());
    await dispatch(shipsActions.getAll({ page }));
    dispatch(loadingActions.stopLoading());
  };
  return (
    <div>
      <div className="row">
        <div className="col-12 mb-3 mb-lg-5">
          <div className="position-relative card table-nowrap table-card">
            <div className="card-header d-flex align-items-center justify-content-between">
              <h5 className="mb-0">Ships</h5>
              <button
                type="button"
                className="btn btn-outline-secondary"
                onClick={() => dispatch(modalActions.openModal())}
              >
                Add new
              </button>
            </div>
            {ships?.items?.length > 0 && (
              <Ships ships={ships} handlePageChange={handlePageChange} />
            )}
            {(ships?.items?.length ?? 0) == 0 && <Empty />}
          </div>
        </div>
      </div>

      {ships?.error && (
        <div className="alert alert-danger text-center">
          {ships.error.message}
        </div>
      )}
    </div>
  );
}

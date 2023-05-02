import { useDispatch } from 'react-redux';
import { shipsActions, modalActions } from '../_store';
import { ShipForm } from './index';

const Modal = () => {
  const dispatch = useDispatch();

  const afterSubmit = async () => {
    await dispatch(shipsActions.getAll(1));
    await dispatch(modalActions.closeModal());
  };

  return (
    <div className="modal fade show" tabIndex="-1" style={{ display: 'block' }}>
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Create a ship</h5>
          </div>
          <div className="modal-body">
            {' '}
            <ShipForm shipId={null} afterSubmit={afterSubmit} />
          </div>
        </div>
      </div>
      <div className="modal-backdrop fade show"></div>
    </div>
  );
};
export { Modal };

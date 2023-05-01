import { useDispatch } from 'react-redux';
import { shipsActions, modalActions } from '../_store';
import { ShipForm } from './index';

const Modal = () => {
  const dispatch = useDispatch();

  const afterSubmit = async () => {
    await dispatch(shipsActions.getAll(1));
    await dispatch(modalActions.closeModal());
  };

  return <ShipForm shipId={null} afterSubmit={afterSubmit} />;
};
export { Modal };

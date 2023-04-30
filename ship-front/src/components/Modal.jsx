import { modalActions } from '../_store/index';

import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { useSelector, useDispatch } from 'react-redux';

import { history } from '../_helpers';
import { shipUpsertActions } from '../_store';

const Modal = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    // redirect to home if already logged in
    //if (authUser) history.navigate('/');
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // form validation rules
  const validationSchema = Yup.object().shape({
    name: Yup.string().required('Name is required'),
  });
  const formOptions = {
    resolver: yupResolver(validationSchema),
  };

  // get functions to build form with useForm() hook
  const { register, handleSubmit, formState } = useForm(formOptions);
  const { errors, isSubmitting } = formState;

  function onSubmit({ name, width, length, code }) {
    return dispatch(
      shipUpsertActions.createShip({ name, width, length, code })
    );
  }

  return (
    <>
      <div
        className="modal fade show"
        tabindex="-1"
        style={{ display: 'block' }}
      >
        <div className="modal-dialog modal-dialog-centered">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">Modal title</h5>
            </div>
            <div className="modal-body">
              <form onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                  <label>Name</label>
                  <input
                    name="name"
                    type="text"
                    {...register('name')}
                    className={`form-control  mt-1 ${
                      errors.name ? 'is-invalid' : ''
                    }`}
                  />
                  <div className="invalid-feedback">{errors.name?.message}</div>
                </div>

                <div className="mt-3 text-center">
                  <button disabled={isSubmitting} className="btn btn-primary">
                    {isSubmitting && (
                      <span className="spinner-border spinner-border-sm mr-1"></span>
                    )}
                    Submit
                  </button>
                </div>
                {/* {error && (
              <div className="alert alert-danger mt-3 mb-0">
                {error.message}
              </div>
            )} */}
              </form>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-secondary"
                onClick={() => {
                  dispatch(modalActions.closeModal());
                }}
              >
                confirm
              </button>
              <button
                type="button"
                className="btn btn-primary"
                onClick={() => {
                  dispatch(modalActions.closeModal());
                }}
              >
                cancel
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="modal-backdrop fade show"></div>
    </>
  );
};
export { Modal };

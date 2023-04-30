import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { useSelector, useDispatch } from 'react-redux';

import { history } from '../_helpers';
import { shipUpsertActions, shipsActions, modalActions } from '../_store';

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
    width: Yup.number()
      .integer()
      .typeError('Width must be a number')
      .required('Width is required.')
      .positive(),

    length: Yup.number()
      .integer()
      .typeError('Length must be a number')
      .required('Length is required.')
      .positive(),
    shipCode: Yup.string().matches(/^[a-zA-Z]{4}-\d{4}-[a-zA-Z]{1}\d{1}/, {
      message: "Code must match 'AAAA-1111-A1'",
      excludeEmptyString: true,
    }),
  });
  const formOptions = {
    resolver: yupResolver(validationSchema),
  };

  const { shipUpsert } = useSelector(x => x.shipUpsert);
  // get functions to build form with useForm() hook
  const { register, handleSubmit, formState } = useForm(formOptions);
  const { errors, isSubmitting } = formState;

  async function onSubmit({ name, width, length, shipCode }) {
    await dispatch(
      shipUpsertActions.createShip({ name, width, length, shipCode })
    );
    await dispatch(shipsActions.getAll(1));
    await dispatch(modalActions.closeModal());
  }

  return (
    <>
      <div
        className="modal fade show"
        tabIndex="-1"
        style={{ display: 'block' }}
      >
        <div className="modal-dialog modal-dialog-centered">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">Modal title</h5>
            </div>
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className="modal-body">
                {shipUpsert?.error && (
                  <div className="text-danger">
                    Error: {shipUpsert.error.message}
                  </div>
                )}
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
                <div className="form-group mt-2">
                  <label>Code</label>
                  <input
                    name="shipCode"
                    type="text"
                    {...register('shipCode')}
                    className={`form-control  mt-1 ${
                      errors.shipCode ? 'is-invalid' : ''
                    }`}
                  />
                  <div className="invalid-feedback">
                    {errors.shipCode?.message}
                  </div>
                </div>
                <div className="form-group mt-2">
                  <label>Length</label>
                  <input
                    name="length"
                    type="text"
                    {...register('length')}
                    className={`form-control  mt-1 ${
                      errors.length ? 'is-invalid' : ''
                    }`}
                  />
                  <div className="invalid-feedback">
                    {errors.length?.message}
                  </div>
                </div>
                <div className="form-group mt-2">
                  <label>Width</label>
                  <input
                    name="width"
                    type="text"
                    {...register('width')}
                    className={`form-control  mt-1 ${
                      errors.width ? 'is-invalid' : ''
                    }`}
                  />
                  <div className="invalid-feedback">
                    {errors.width?.message}
                  </div>
                </div>

                {/* {error && (
              <div className="alert alert-danger mt-3 mb-0">
                {error.message}
              </div>
            )} */}
              </div>
              <div className="modal-footer d-flex align-items-center justify-content-between">
                <button
                  disabled={isSubmitting}
                  type="submit"
                  className="btn btn-primary"
                >
                  {isSubmitting && (
                    <span className="spinner-border spinner-border-sm mr-1"></span>
                  )}
                  Submit
                </button>
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => {
                    dispatch(modalActions.closeModal());
                  }}
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div className="modal-backdrop fade show"></div>
    </>
  );
};
export { Modal };

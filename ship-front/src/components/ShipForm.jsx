import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { yupResolver } from '@hookform/resolvers/yup';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { singleShipActions, shipUpsertActions } from '../_store';
import { Loading } from '../components';

function ShipForm({ shipId, afterSubmit }) {
  const dispatch = useDispatch();
  const { ship } = useSelector(x => x.ship);

  useEffect(() => {
    if (shipId) getShip(parseInt(shipId));

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    let defaultValues = {
      id: ship.id,
      name: ship?.name,
      shipCode: ship?.shipCode,
      width: ship?.width,
      length: ship?.length,
    };
    reset({ ...defaultValues });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [ship]);
  const getShip = async shipId => {
    await dispatch(singleShipActions.GetById({ shipId }));
  };

  const codeRegex = /^[a-zA-Z]{4}[-]{1}\d{4}[-]{1}[a-zA-Z]{1}\d{1}$/;
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
    shipCode: Yup.string().matches(codeRegex, {
      message: "Code must match 'AAAA-1111-A1'",
      excludeEmptyString: true,
    }),
  });
  const formOptions = {
    resolver: yupResolver(validationSchema),
  };

  const { shipUpsert } = useSelector(x => x.shipUpsert);
  const { register, handleSubmit, formState, reset } = useForm(formOptions);
  const { errors, isSubmitting } = formState;

  async function onSubmit({ id, name, width, length, shipCode }) {
    await dispatch(
      shipUpsertActions.createShip({ id, name, width, length, shipCode })
    );
    if (ship.added) afterSubmit();
  }

  return (
    <div className="modal fade show" tabIndex="-1" style={{ display: 'block' }}>
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Create a ship</h5>
          </div>
          <div className="modal-body">
            {shipId && <h1 className="text-center">{ship.name}</h1>}
            <form onSubmit={handleSubmit(onSubmit)}>
              {shipId && <input type="hidden" {...register('id')} name="id" />}

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
                    maxLength="12"
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
                    type="number"
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
                    type="number"
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
              </div>
            </form>
            <h3>xasxa: {ship?.error}</h3>
          </div>
        </div>
      </div>
      <div className="modal-backdrop fade show"></div>
    </div>
  );
}

export { ShipForm };

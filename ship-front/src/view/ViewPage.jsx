import { useParams } from 'react-router-dom';
import { history } from '../_helpers';
import { ShipForm } from '../components';

function ViewPage() {
  const shipId = useParams().id;

  const afterSubmit = () => history.navigate('/');

  return (
    <div className="row justify-content-center">
      <div className="col-12 col-md-8 col-lg-6">
        <ShipForm shipId={shipId} afterSubmit={afterSubmit} />
      </div>
    </div>
  );
}

export { ViewPage };

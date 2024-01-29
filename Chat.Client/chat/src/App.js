import Chat from './features/Chat/Chat';
import Sign from './features/Sign/Sign';
import './App.css';
import { useDispatch, useSelector } from 'react-redux';
import { loadUser, selectLogged } from './app/User/userSlice';
import { useEffect } from 'react';

function App() {
  const
    dispatch = useDispatch(),
    isLogged = useSelector(selectLogged);

  useEffect(() => {
    dispatch(loadUser());
  }, [dispatch]);

  return (
    <div className="main">
      {
        isLogged
          ? <Chat />
          : <Sign />
      }
    </div>
  );
}

export default App;

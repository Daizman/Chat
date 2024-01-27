import Chat from './features/Chat/Chat';
import Sign from './features/Sign/Sign';
import useUser from './hooks/useUser';
import './App.css';

function App() {
  const isUserLoggedIn = useUser() != null;

  return (
    <div className="main">
      {
        isUserLoggedIn
          ? <Chat />
          : <Sign />
      }
    </div>
  );
}

export default App;

import './App.css';
import ProductTable from './ProductTable';
import ChecksTable from './ChecksTable';
import Header from './Header';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div className="main-conteiner">
      <Router>
        <div className="header-conteiner">
          <Header />
        </div>        
        <div className="body-conteiner">
          <Routes>
            <Route path="/" element={<ProductTable />} />
            <Route path="/products" element={<ProductTable />} />
            <Route path="/checks" element={<ChecksTable />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;

import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';
import './ChecksTable.css';

function ChecksTable() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');
  const searchInputRef = useRef(null);

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        const response = await axios.get('http://localhost:5088/v1/checks', {
          params: { query: searchQuery }
        });
        setProducts(response.data);
        setLoading(false);
      } catch (error) {
        setError(error);
        setLoading(false);
      }
    };

    fetchProducts();
  }, [searchQuery]);

  useEffect(() => {
    if (searchInputRef.current) {
      searchInputRef.current.focus();
    }
  }, [products, loading, error]);

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this item?')) {
      try {
        await axios.delete(`http://localhost:5088/v1/checks/${id}`);
        setProducts(products.filter(product => product.id !== id));
      } catch (error) {
        setError(error);
      }
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div>
      <div className="search-container">
        <input
          id='search-input'
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          ref={searchInputRef}
          className="search-input"
        />
      </div>
      <div className="table-container">
        <table>
          <thead className="table-header">
            <tr>
              <th>Id</th>
              <th>Product</th>
              <th>Date</th>
              <th>Count</th>
              <th>Price</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody className="table">
            {products.map(check => (
              <tr key={check.id}>
                <td>{check.id}</td>
                <td>{check.productId}</td>
                <td>{check.date}</td>
                <td>{check.count}</td>
                <td>{check.price}</td>
                <td>
                  <button onClick={() => handleDelete(check.id)}>-</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default ChecksTable;

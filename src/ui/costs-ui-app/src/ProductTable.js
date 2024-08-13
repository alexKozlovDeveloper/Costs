import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';
import './ProductTable.css';

function ProductTable() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedProductId, setSelectedProductId] = useState(null);
  const [formData, setFormData] = useState({ date: '', price: '', count: '' });
  const searchInputRef = useRef(null);

  useEffect(() => {
    setFormData((prevData) => ({
      ...prevData,
      date: new Date().toISOString().split('T')[0]
    }));
  }, []);

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        const response = await axios.get('http://localhost:5088/v1/products', {
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

  const openModal = (id) => {
    setSelectedProductId(id);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setFormData({ field1: '', field2: '' });
  };

  const handleFormChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:5088/v1/checks', {
        ...formData,
        productId: selectedProductId
      });
      closeModal();
    } catch (error) {
      setError(error);
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
              <th>Description</th>
              <th>Category</th>
              <th>Tags</th>
              <th>Weight</th>
              <th>Energy Value</th>
              <th>Proteins</th>
              <th>Fats</th>
              <th>Carbohydrates</th>
              <th>Product Energy Value</th>
              <th>Add Check</th>
            </tr>
          </thead>
          <tbody className="table">
            {products.map(product => (
              <tr key={product.id}>
                <td>{product.id}</td>
                <td>{product.description}</td>
                <td>{product.category}</td>
                <td>{product.tags}</td>
                <td>{product.weight}</td>
                <td>{product.energyValue}</td>
                <td>{product.proteins}</td>
                <td>{product.fats}</td>
                <td>{product.carbohydrates}</td>
                <td>{product.productUnitEnergyValue}</td>
                <td>
                  <button onClick={() => openModal(product.id)}>+</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {isModalOpen && (
        <div className="modal-overlay">
          <div className="modal-content">
            <h2>New Product</h2>
            <form onSubmit={handleSubmit}>
              <label>
                Date:
                <input
                  type="date"
                  name="date"
                  value={formData.date}
                  onChange={handleFormChange}
                />
              </label>
              <br />
              <label>
                Price:
                <input
                  type="number"
                  name="price"
                  value={formData.price}
                  onChange={handleFormChange}
                />
              </label>
              <br />
              <label>
                Count:
                <input
                  type="number"
                  name="count"
                  value={formData.count}
                  onChange={handleFormChange}
                />
              </label>
              <button type="submit">Submit</button>
              <button type="button" onClick={closeModal}>Cancel</button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}

export default ProductTable;

import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ProductTable.css';

function ProductTable() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('http://localhost:5088/v1/products')
      .then(response => {
        setProducts(response.data);
        setLoading(false);
      })
      .catch(error => {
        setError(error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div className="table-container">
        <table>
            <thead className="table-header">
                <tr>
                <th>Id</th>
                <th>Description</th>
                <th>Category</th>
                <th>Tags</th>
                <th>Weight</th>
                <th>CaloriesPer100g</th>
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
                    <td>{product.caloriesPer100g}</td>
                </tr>
                ))}
            </tbody>
        </table>
    </div>
  );
}

export default ProductTable;
import React, { useState } from 'react';
// import './CheckoutPage.css'; // Optional: import for CSS styling

const Checkout = () => {
  const [cartItems, setCartItems] = useState([
    { id: 1, name: 'Product A', price: 20, quantity: 2 },
    { id: 2, name: 'Product B', price: 10, quantity: 1 },
  ]);

  const calculateTotal = () => {
    return cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  };

  const handleCheckout = (e) => {
    e.preventDefault();
    alert("Payment submitted");
    // Here you could add functionality to integrate with a payment processor
  };

  return (
    <div className="checkout-page">
      <h2>Checkout</h2>
      <div className="cart-items">
        <h3>Your Cart</h3>
        {cartItems.map(item => (
          <div key={item.id} className="cart-item">
            <span>{item.name}</span>
            <span>{item.quantity} x ${item.price}</span>
          </div>
        ))}
      </div>
      
      <div className="total-amount">
        <h3>Total: ${calculateTotal()}</h3>
      </div>

      <form onSubmit={handleCheckout} className="payment-form">
        <h3>Payment Information</h3>
        <label>
          Card Number
          <input type="text" name="cardNumber" required />
        </label>
        <label>
          Expiry Date
          <input type="text" name="expiryDate" required />
        </label>
        <label>
          CVV
          <input type="text" name="cvv" required />
        </label>
        <button type="submit">Place Order</button>
      </form>
    </div>
  );
};

export default Checkout;

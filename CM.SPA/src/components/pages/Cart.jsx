import React, { useState, useEffect } from 'react';

const Cart = ({ cartId }) => {
  const [cartItems, setCartItems] = useState([]);
  const [total, setTotal] = useState(0);
  const [isCheckingOut, setIsCheckingOut] = useState(false);
  const [checkoutMessage, setCheckoutMessage] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  // Fetch cart items from the API
  useEffect(() => {
    const fetchCartItems = async () => {
      try {
        setIsLoading(true);
        setError('');
        const response = await fetch(`/api/cart/GetCartById/${cartId}`);
        if (!response.ok) {
          throw new Error('Failed to fetch cart items');
        }
        const data = await response.json();
        setCartItems(data.cartItems || []);
        setIsLoading(false);
      } catch (error) {
        setError(error.message);
        setIsLoading(false);
      }
    };

    fetchCartItems();
  }, [cartId]); // Fetch cart items whenever cartId changes

  // Update total price whenever the cart items change
  useEffect(() => {
    const totalPrice = cartItems.reduce((acc, item) => acc + item.quantity * item.price, 0);
    setTotal(totalPrice);
  }, [cartItems]);

  const updateQuantity = (ticketId, newQuantity) => {
    setCartItems((prevItems) =>
      prevItems.map((item) =>
        item.ticketId === ticketId ? { ...item, quantity: newQuantity } : item
      )
    );
  };

  const removeTicket = (ticketId) => {
    setCartItems((prevItems) => prevItems.filter((item) => item.ticketId !== ticketId));
  };

  const handleCheckout = () => {
    if (cartItems.length === 0) {
      setCheckoutMessage("Your cart is empty. Add items before checking out.");
      return;
    }

    setIsCheckingOut(true);
    setCheckoutMessage("");

    setTimeout(() => {
      setCartItems([]);
      setIsCheckingOut(false);
      setCheckoutMessage("Checkout successful! Thank you for your purchase.");
    }, 2000); // Simulate a checkout delay
  };

  if (isLoading) {
    return <p>Loading cart items...</p>;
  }

  if (error) {
    return <p>Error loading cart: {error}</p>;
  }

  return (
    <div className="cart-container">
      <h2>Your Cart</h2>
      {cartItems.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <>
          <table className="cart-table">
            <thead>
              <tr>
                <th>Movie</th>
                <th>Showtime</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Total</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {cartItems.map((item) => (
                <tr key={item.ticketId}>
                  <td>{item.movieTitle}</td>
                  <td>{item.showtime}</td>
                  <td>
                    <input
                      type="number"
                      value={item.quantity}
                      min="1"
                      onChange={(e) => updateQuantity(item.ticketId, parseInt(e.target.value, 10))}
                    />
                  </td>
                  <td>${item.price.toFixed(2)}</td>
                  <td>${(item.quantity * item.price).toFixed(2)}</td>
                  <td>
                    <button onClick={() => removeTicket(item.ticketId)}>Remove</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="cart-total">
            <h3>Total: ${total.toFixed(2)}</h3>
          </div>
          <button className="checkout-button" onClick={handleCheckout} disabled={isCheckingOut}>
            {isCheckingOut ? 'Processing...' : 'Proceed to Checkout'}
          </button>
          {checkoutMessage && <p className="checkout-message">{checkoutMessage}</p>}
        </>
      )}
    </div>
  );
};

export default Cart;

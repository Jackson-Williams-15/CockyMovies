import React from 'react';
import { useParams } from 'react-router-dom';
import OrderReceipt from './OrderReceipt';

export default function OrderSuccess() {
  const { orderId } = useParams();
  console.log('orderId:', orderId); 

  return (
    <div>
      <h1>Order Success</h1>
      <OrderReceipt orderId={orderId} />
    </div>
  );
}
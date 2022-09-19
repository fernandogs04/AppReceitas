import React from 'react';
import Cookies from 'universal-cookie';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { Cabecalho } from './components';
import { Auth, Receitas, NovaReceita, Detalhes  } from './pages';

import './App.css';

const cookies = new Cookies();

const authToken = cookies.get("token");

const App = () => {

  if (!authToken) return <Auth />

  return (
    <BrowserRouter>
      <Cabecalho />
      <Routes>
        <Route path="/" element={
          <Receitas />
        }/>
        <Route path="/novo" element={
          <NovaReceita />
        }/>
        <Route path="/detalhes/:id" element={
          <Detalhes />
        }/>
      </Routes>
    </BrowserRouter>
  )
}

export default App
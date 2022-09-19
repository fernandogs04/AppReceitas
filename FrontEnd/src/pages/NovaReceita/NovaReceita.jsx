import React, { useState } from 'react'
import Cookies from 'universal-cookie';
import axios from 'axios';
import { BotaoVoltar } from '../../components';
import { URLServer } from '../../config/URLServer';
import { useNavigate } from 'react-router-dom';

import { PaginaNovaReceita } from './styles';

const cookies = new Cookies();

const initialState = {
  nomeReceita: '',
  descricao: '',
  ingredientes: '',
  instrucoes: ''
}

const NovaReceita = () => {
  const [form, setForm] = useState(initialState);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    
    const { nomeReceita, descricao, ingredientes, instrucoes } = form;

    const config = {
      NomeReceita: nomeReceita,
      Descricao: descricao,
      Ingredientes: ingredientes,
      Instrucoes: instrucoes
    }

    const JWToken = {
      headers: { Authorization: `Bearer ${cookies.get('token')}` }
    }

    await axios.post(`${URLServer}/receitas`, config, JWToken)
    .then(({ data: { message } }) => {
      alert("Receita criada com sucesso");
      navigate("/");
    })
    .catch(res => {
      if (res.response.status === 401) {
          cookies.remove('token', { path: '/' });
          window.location.reload();
      }
    })
  }


  return (
    <PaginaNovaReceita>
      <BotaoVoltar />
      <div className='tituloForm'>
        <p>Nova Receita</p>
      </div>
      <div className='menuForm'>
        <form onSubmit={handleSubmit}>
          <label htmlFor='nomeReceita'>
            <span>Nome Da Receita</span>
            <input
              name='nomeReceita'
              type='text'
              placeholder='Insira o nome da receita'
              onChange={handleChange}
              required
            />
          </label>
          <label htmlFor='descricao'>
            <span>Descrição</span>
            <textarea
              name='descricao'
              placeholder='Insira a descrição'
              onChange={handleChange}
              required
            />
          </label>
          <label htmlFor='ingredientes'>
            <span>Ingredientes</span>
            <textarea
              name='ingredientes'
              placeholder='Insira os ingredientes'
              onChange={handleChange}
              required
            />
          </label>
          <label htmlFor='instrucoes'>
            <span>Instruções</span>
            <textarea
              name='instrucoes'
              placeholder='Insira as instruções de preparo'
              onChange={handleChange}
              required
            />
          </label>
          <button>Registrar Receita</button>
        </form>
      </div>
    </PaginaNovaReceita>
  )
}

export default NovaReceita
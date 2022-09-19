import React, { useState } from 'react';
import Cookies from 'universal-cookie';
import axios from 'axios';
import { URLServer } from '../../config/URLServer';

import { PaginaForm } from './styles';

const cookies = new Cookies();

const initialState = {
  nome: '',
  senha: ''
}

const Auth = () => {
  const [form, setForm] = useState(initialState);
  const [isSignup, setIsSignup] = useState(false);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e) => {
    e.preventDefault();

    const { nome, senha } = form;

    // Fazer cadastro ou login
    await axios.post(`${URLServer}/${isSignup ? "registrar" : "login"}`,
    {
      Nome: nome,
      Senha: senha
    })
    .then(({ data: { token } }) => {
      cookies.set('token', token, { path: "/" })
      
      window.location.reload();
    })
    .catch((e) => {
      alert("Senha errada")
    })
  }

  const switchMode = () => {
    setIsSignup((prevIsSignup) => !prevIsSignup);
  }

  return (
    <PaginaForm>
      <div className='tituloForm'>
        <p>Fazer {isSignup ? 'Cadastro' : 'Login'}</p>
      </div>
      <div className='menuForm'>
        <form onSubmit={handleSubmit}>
          <label htmlFor='nome'>
            <span>Nome</span>
            <input
              name='nome'
              type='text'
              placeholder='Insira seu nome'
              onChange={handleChange}
              required
            />
          </label>
          <label htmlFor='senha'>
            <span>Senha</span>
            <input
              name='senha'
              type='text'
              placeholder='Insira sua senha'
              onChange={handleChange}
              required
            />
          </label>
          <button>{isSignup ? 'Fazer Cadastro' : 'Fazer Login'}</button>
          <div>
            <p>
              {isSignup
                ? 'Já tem uma conta?'
                : 'Não tem uma conta?'
              }
              <span className='mudarFormulario' onClick={switchMode}>
                {isSignup
                  ? 'Fazer Login'
                  : 'Fazer Cadastro'
                }
              </span>
            </p>
          </div>
        </form>
      </div>
    </PaginaForm>
  )
}

export default Auth
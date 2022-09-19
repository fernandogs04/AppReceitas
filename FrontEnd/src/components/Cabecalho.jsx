import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

const Cabecalho = () => {
  const logout = () => {
    cookies.remove('token', { path: '/' });

    window.location.reload();
  }

  return (
    <DivCabecalho>
      <Link to={`/`}>
        <i className="fa-solid fa-bowl-food"></i>
      </Link>
      <button onClick={logout}>
        <i className="fa-solid fa-right-from-bracket"></i>
      </button>
    </DivCabecalho>
  )
}

const DivCabecalho = styled.div`
  padding: 1rem;
    
  display: flex;
  justify-content: space-between;

  border-bottom: 1px solid var(--cor30);

  & a,
  & a:visited,
  & button {
    font-size: 200%;
    background-color: transparent;
    color: var(--cor10);

    cursor: pointer;

    transition: all .3s;
  }

  & a:hover,
  & button:hover {
    transform: scale(1.1);
  }
`

export default Cabecalho
import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

const Receita = ({ receita }) => {
  return (
    <LinkReceita to={`/detalhes/${receita.idreceita}`}>
      <h1>{receita.nomereceita}</h1>
      <span>{receita.descricao}</span>
    </LinkReceita>
  )
}

const LinkReceita = styled(Link)`
  padding: 1rem;

  display: flex;
  flex-direction: column;
  align-items: center;

  text-align: center;

  text-decoration: none;

  border-radius: 10px;

  background-color: var(--cor30);

  transition: all .3s;

  &,
  &:visited {
  color: white;
  }

  &:hover {
  transform: scale(1.05, 1.1);
  }

  & h1 {
    color: var(--cor10);
  }
`

export default Receita
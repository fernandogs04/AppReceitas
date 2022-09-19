import React from 'react'
import { Link } from 'react-router-dom'
import styled from 'styled-components';

const BotaoVoltar = () => {
  return (
    <LinkBotaoVoltar to='/'>
      <i className="fa-solid fa-arrow-left"></i>
      <span>Voltar</span>
    </LinkBotaoVoltar>
  )
}

const LinkBotaoVoltar = styled(Link)`
  padding: 1rem;

  position: sticky;
  top: 0;

  align-self: flex-start;

  font-size: 1rem;

  color: black;
  text-decoration: none;

  background-image: linear-gradient(var(--cor10), rgba(255, 255, 255, 0));

  transition: all .3s;

  &:hover {
    background-color: var(--cor10);
  }

  & span {
    margin-left: .5rem;
  }
`

export default BotaoVoltar
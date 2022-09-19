import React from 'react'
import { Link } from 'react-router-dom'
import styled from 'styled-components'

const BotaoNovaReceita = () => {
  return (
    <Link to='/novo'>
      <NovaReceita>
        <i className="fa-solid fa-plus"></i>
      </NovaReceita>
    </Link>
  )
}

const NovaReceita = styled.button`
  height: 5rem;
  aspect-ratio: 1 / 1;

  position: fixed;
  bottom: 2rem;
  right: 2rem;

  cursor: pointer;

  font-size: 200%;

  border-radius: 50px;

  background-color: var(--cor10);

  transition: all .3s;

  &:hover {
    transform: scale(1.1);
  }
`

export default BotaoNovaReceita
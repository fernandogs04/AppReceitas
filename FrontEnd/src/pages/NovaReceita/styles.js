import styled from "styled-components";

export const PaginaNovaReceita = styled.div`
  width: calc(100vw - (100vw - 100%));

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  .menuForm {
    color: black;
    width: 40vw;

    margin-bottom: 2rem;

    transition: all 0.5s;
  }

  .tituloForm {
    min-height: 20vh;
    line-height: 20vh;
    font-size: 5rem;
    text-align: center;
    color: var(--cor10);
    margin-bottom: 3rem;
  }

  .menuForm form {
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    align-items: center;
    min-height: 50vh;
    font-size: 1.1rem;

    padding: 1rem;

    background-color: var(--cor30);
    border-radius: 10px;
  }

  .menuForm form label {
    display: flex;
    flex-direction: column;

    width: 75%;

    margin-bottom: 1rem;
  }

  .menuForm form span {
    margin-bottom: 0.5rem;
  }

  .menuForm form label span {
    color: var(--branco);
  }

  .menuForm form label input,
  .menuForm form label textarea {
    height: 50px;
    padding: 0 5px;

    outline: none;

    font-size: 1.1rem;
    font-family: var(--fonte);
    color: var(--cor30);
    background-color: var(--branco);
    border-radius: 10px;

    transition: all .1s;
  }

  .menuForm form label textarea {
    height: 150px;
    padding: .5rem;
    resize: none
  }

  .menuForm form label input:focus,
  .menuForm form label textarea:focus {
    outline: 2px solid var(--cinza);
  }

  .menuForm form button {
    background-color: var(--cor10);
    height: 50px;
    width: 75%;
    font-size: 1.2rem;
    padding: 0 5px;
    border-radius: 10px;
    margin-bottom: 0.5rem;

    cursor: pointer;

    transition: all 0.3s;
  }

  .menuForm form button:hover {
    transform: scale(1.1);
  }

  .menuForm form div p {
    color: var(--branco);
  }

  .menuForm form .mudarFormulario {
    color: var(--cor10);
    margin-left: 0.5rem;
    cursor: pointer;
  }

  @media (max-width: 576px) {
    .paginaForm {
      justify-content: flex-start;
    }

    .tituloForm {
      line-height: 1;
      margin: 2rem 0;
    }

    .menuForm {
      width: 90vw;
    }

    .menuForm form label {
      width: 90%;
    }

    .menuForm form button {
      width: 90%;
    }
  }

  @media (min-width: 576px) and (max-width: 768px) {
    .menuForm {
      width: 60vw;
    }
  }
`
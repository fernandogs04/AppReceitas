import React, { useState, useEffect, useRef } from 'react'
import { useParams, useNavigate } from "react-router-dom";
import Cookies from 'universal-cookie';
import axios from 'axios';
import { BotaoVoltar } from '../../components';
import { URLServer } from '../../config/URLServer';

import { PaginaDetalhes } from './styles';

const cookies = new Cookies();

const Detalhes = () => {
  const { id } = useParams();

  const [receita, setReceita] = useState({});
  const [isEdit, setIsEdit] = useState(false);

  const navigate = useNavigate();

  let receitaOriginal = useRef();
    
  useEffect(() => {
    const config = {
      headers: { Authorization: `Bearer ${cookies.get('token')}` }
    };

    axios.get(`${URLServer}/receitas/${id}`, config)
      .then(res => {
        setReceita(res.data);
        receitaOriginal.current = res.data;
      })
  }, [id])

  const updateReceita = () => {
    const config = {
      Nomereceita: receita.nomereceita,
      Descricao: receita.descricao,
      Ingredientes: receita.ingredientes,
      Instrucoes: receita.instrucoes
    };

    axios.put(`${URLServer}/receitas/${id}`, config, {
      headers: { Authorization: `Bearer ${cookies.get('token')}`
    }})
      .then(res => {
        setReceita(res.data);
        receitaOriginal.current = res.data;
        
        switchMode();
        alert("Receita editada com sucesso");
      })
  }

  const deleteReceita = () => {
    if (window.confirm("Você tem certeza que quer deletar esta receita?") === true) {
      const config = {
        headers: { Authorization: `Bearer ${cookies.get('token')}` }
      };
  
      axios.delete(`${URLServer}/receitas/${id}`, config)
        .then(res => {
          switchMode();
          alert("Receita deletada com sucesso!");
          navigate("/");
        })
    }
  }

  const handleChange = (e) => {
    setReceita({ ...receita, [e.target.name]: e.target.value })
  }

  const switchMode = () => {
    setIsEdit((prevIsEdit) => {
      if(prevIsEdit)
      {
        setReceita(receitaOriginal.current);
      }

      return !prevIsEdit
    });
  }

  return (
    <PaginaDetalhes>
      <BotaoVoltar />
      {!isEdit && (
        <div className='tituloForm'>
          <p>
            {receita.nomereceita}
          </p>
        </div>
      )}
      <div className='menuForm'>
        {isEdit && (
          <label htmlFor='nomereceita'>
            <span>Nome</span>
            <textarea
              name='nomereceita'
              value={receita.nomereceita}
              onChange={handleChange}
              readOnly={!isEdit}
            />
          </label>
        )}
        <label htmlFor='descricao'>
          <span>Descrição</span>
          <textarea
            name='descricao'
            value={receita.descricao}
            onChange={handleChange}
            readOnly={!isEdit}
          />
        </label>
        <label htmlFor='ingredientes'>
          <span>Ingredientes</span>
          <textarea
            name='ingredientes'
            value={receita.ingredientes}
            onChange={handleChange}
            readOnly={!isEdit}
          />
        </label>
        <label htmlFor='instrucoes'>
          <span>Instruções</span>
          <textarea
            name='instrucoes'
            value={receita.instrucoes}
            onChange={handleChange}
            readOnly={!isEdit}
          />
        </label>
        {isEdit && (
          <button onClick={updateReceita}><i className="fa-solid fa-check"></i>  Confirmar Edição</button>
        )}
        <button onClick={switchMode}>
          {isEdit
            ? <><i className="fa-solid fa-xmark"></i>  Cancelar</>
            : <><i className="fa-solid fa-pen"></i>  Editar Receita</>}
        </button>
        {!isEdit && (
          <button onClick={deleteReceita}><i className="fa-solid fa-trash"></i>  Deletar Receita</button>
        )}
      </div>
    </PaginaDetalhes>
  )
}

export default Detalhes
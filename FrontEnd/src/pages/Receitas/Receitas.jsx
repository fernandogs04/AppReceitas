import React, { useState, useEffect } from 'react'
import Cookies from 'universal-cookie';
import axios from 'axios';
import { Receita, BotaoNovaReceita } from '../../components/';
import { URLServer } from '../../config/URLServer';

import { ListaReceitas } from './styles';

const cookies = new Cookies();

const PaginaReceitas = () => {
  const [receitas, setReceitas] = useState([]);

  useEffect(() => {
    const config = {
      headers: { Authorization: `Bearer ${cookies.get('token')}` }
    };

    axios.get(`${URLServer}/receitas`, config)
    .then((res) => {
      setReceitas(res.data.listaReceitas);
    })
    .catch(res => {
      if (res.response.status === 401) {
          cookies.remove('token', { path: '/' });
          window.location.reload();
      }
    })
  }, [])

  return (
    <>
      <ListaReceitas>
        {
          receitas.map((receita) => {
            return(
              <Receita receita={receita} key={receita.idreceita}/>
            )
          })
        }
      </ListaReceitas>
      <BotaoNovaReceita />
    </>
  )
}

export default PaginaReceitas
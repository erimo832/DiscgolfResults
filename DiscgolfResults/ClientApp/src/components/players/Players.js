import { Box, CircularProgress, TextField } from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { NavLink } from 'reactstrap';

export function Players(props) {
  const { i18n } = useTranslation();
  const [state, setState] = useState( {
    loading: true,
    players: null,
    filtered: null
  }); 

  useEffect(() => {
    if(state.players === null)
      fetchData(true);
  });

  let contents = state.loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : renderPlayersTable(state.filtered);

  const handleChange = (event) => {
    let filtered = state.players.filter(x => x.fullName.toLowerCase().includes(event.target.value.toLowerCase()) || x.pdgaNumber.includes(event.target.value));
    
    setState({...state,
          filtered: filtered.map(x => x)
    });
  };

  function renderPlayersTable(players) {
    return (
      <>
        <TextField id="outlined-basic" fullWidth label={i18n.t('common_filter_players')} variant="outlined" onChange={ (e) => handleChange(e) } />
        <table className='table table-condensed table-striped table-sm' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>{i18n.t('column_name')}</th>
              <th>{i18n.t('column_pdgalink')}</th>
            </tr>
          </thead>
          <tbody>
            {players.map(x =>
              <tr key={x.playerId}>
                  <td>                  
                    <NavLink href={"players/" + x.playerId } className="text-dark">{x.firstName + ' ' + x.lastName}</NavLink>
                  </td>
                  <td>
                    <a href={`https://www.pdga.com/player/${x.pdgaNumber}`} target={"_blank"} rel="noopener noreferrer">{x.pdgaNumber}</a>
                  </td>
              </tr>
            )}
          </tbody>
        </table>
      </>
    );
  }
  async function fetchData() {
    const response = await fetch('/api/players');
    const data = await response.json();
    setState({
      loading: false,
      players: data,
      filtered: data.map(x => x)
    })
  }

  return (
    <div>
      <h1 id="tabelLabel">{i18n.t('players_header')}</h1>
      <p>{i18n.t('players_description')}</p>        
      {contents}
    </div>
  );  
}
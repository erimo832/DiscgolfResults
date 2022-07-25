import { Box, CircularProgress, TextField } from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Grid } from './common/Grid';

export function Hcp(props) {
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
    : renderHcpTable(state.filtered);

    const handleChange = (event) => {
      let filtered = state.players.filter(x => x.fullName.toLowerCase().includes(event.target.value.toLowerCase()) || x.pdgaNumber.includes(event.target.value));
      
      setState({...state,
            filtered: filtered.map(x => x)
      });
    };

  function renderHcpTable(list) {
    //

    return (
      <>
        <TextField id="outlined-basic" fullWidth label={i18n.t('common_filter_players')} variant="outlined" onChange={ (e) => handleChange(e) } />
        <Grid data={state.filtered} format={getGridConf()} />
      </>
    );
  }

  function getGridConf() {
    return {
      className: "table table-striped",
      key: "playerId",
      detailsArray: "",
      detailsValue: "",
      columns: [        
        {columnName: "fullName",  headerText: i18n.t('column_name'), headerClassName: "", columnClass: ""},
        {columnName: "currentHcp",headerText: i18n.t('column_hcp'),  headerClassName: "", columnClass: ""}
      ]
    };
  }

  async function fetchData() {
    const response = await fetch('api/players/handicaps');
    const data = await response.json();
    setState({
      loading: false,
      players: data,
      filtered: data.map(x => x)
    })
  }

  return (
    <div>
      <h1 id="tabelLabel">{i18n.t('hcp_header')}</h1>
      <p>{i18n.t('hcp_description_avgscore')}</p>
      <p>{i18n.t('hcp_description_hcp')}</p>
      {contents}
    </div>
  );
}
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import i18n from "../../i18n";
import { Grid } from '../common/Grid';
import Collapse, { Panel } from 'rc-collapse';
import { useParams } from 'react-router-dom';


export function Details() {
  const [loading, setLoading] = useState(true);
  const [rounds, setRounds] = useState(null);
  let params = useParams(rounds);

  let contents = loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : renderDetailsTable(rounds);

  useEffect(() => {
    if(rounds === null)
      fetchData();

    //TODO: Add highligting on included events in calculation and what are used for avg cals
  });

  function renderDetailsTable(info) {
    if(info.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);
    
    return (
        <div>
          <h1>{info.fullName}</h1>
          <Grid data={info.eventResults} format={getGridConf()} />
        </div>
    );
  }

  function getGridConf()
  {
    return {
      className: "table table-striped",
      key: "eventId",
      detailsArray: "",
      detailsValue: "",
      columns: [
        {columnName: "eventName",   headerText: i18n.t('column_round'),    headerClassName: "", rowClassName: ""},
        {columnName: "startTime",   headerText: i18n.t('column_date'),     headerClassName: "", rowClassName: "", formatedValue: function(x) {  return x.startTime.substring(0,10); }},
        {columnName: "placement",   headerText: i18n.t('column_place'),    headerClassName: "", rowClassName: ""},
        {columnName: "points",      headerText: i18n.t('column_points'),   headerClassName: "", rowClassName: ""},
        {columnName: "score",       headerText: i18n.t('column_score'),    headerClassName: "", rowClassName: ""},
        {columnName: "hcpBefore",   headerText: i18n.t('column_hcp'),      headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},
        {columnName: "hcpScore",    headerText: i18n.t('column_hcpscore'), headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"}       
      ]
    };
  }

  async function fetchData() {
    var path = '/api/players/'+ params.playerId + '/details';
    const response = await fetch(path);
    const data = await response.json();

    setRounds(data);
    setLoading(false);
  };

  return (
    <div>         
        {contents}
    </div>
  );
}

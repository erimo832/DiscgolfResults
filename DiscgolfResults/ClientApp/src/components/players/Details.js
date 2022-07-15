import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import i18n from "../../i18n";
import { useParams } from 'react-router-dom';


export function Details() {
  const [loading, setLoading] = useState(true);
  const [rounds, setRounds] = useState();
  let params = useParams(rounds);

  let contents = loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : renderDetailsTable(rounds);

  useEffect(() => {
    fetchData();
  });

  function renderDetailsTable(info) {
    if(info.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);

    return (
        <div>
            <div></div>
            <table className='table table-condensed table-sm' aria-labelledby="tabelLabel">
            <thead>
                <tr>                    
                    <th className="d-none d-lg-table-cell">{i18n.t('column_round')}</th>
                    <th>{i18n.t('column_date')}</th>
                    <th>{i18n.t('column_place')}</th>
                    <th>{i18n.t('column_points')}</th>
                    <th>{i18n.t('column_score')}</th>
                    <th className="d-none d-sm-table-cell">{i18n.t('column_hcp')}</th>
                    <th className="d-none d-sm-table-cell">{i18n.t('column_hcpscore')}</th>
                </tr>
            </thead>
            <tbody>
                {info.map(x =>
                <tr key={x.startTime} className={x.inHcpAvgCalculation ? 'avg' : (x.inHcpCalculation === true ? 'top' : 'none' ) }>                    
                    <td className="d-none d-lg-table-cell">x.eventName</td>
                    <td>{x.startTime.substring(0,10)}</td>
                    <td>x.placemenet</td>
                    <td>x.roundPoints</td>
                    <td>{x.score}</td>
                    <td className="d-none d-sm-table-cell">x.hcp</td>
                    <td className="d-none d-sm-table-cell">x.hcpScore</td>
                </tr>
                )}
            </tbody>
            </table>
        </div>
    );
  }

  async function fetchData() {
    var path = '/api/players/'+ params.playerId + '/round-scores';
    const response = await fetch(path);
    const data = await response.json();

    setRounds(data);
    setLoading(false);
  };

  return (
    <div>
        <h1 id="tabelLabel">{i18n.t('players_header')}</h1>
        <p>{i18n.t('players_description')}</p>        
        {contents}
      </div>
  );
}

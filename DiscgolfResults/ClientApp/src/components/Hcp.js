import { Box, CircularProgress } from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import i18n from "../i18n";
import { Grid } from './common/Grid';

export class Hcp extends Component {
  static displayName = Hcp.name;

  constructor(props) {
    super(props);
    this.state = { hcp: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderHcpTable(hcpList) {
    return (
      <Grid data={this.getDataForGrid(hcpList)} format={this.getGridConf()} />
    );
  }

  getGridConf()
  {
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

  getDataForGrid(data)
  {
    return data;
  }

  render() {    
    let contents = this.state.loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : this.renderHcpTable(this.state.hcp);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('hcp_header')}</h1>
        <p>{i18n.t('hcp_description_avgscore')}</p>
        <p>{i18n.t('hcp_description_hcp')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('api/players/handicaps');
    const data = await response.json();
    this.setState({ hcp: data, loading: false });
  }
}
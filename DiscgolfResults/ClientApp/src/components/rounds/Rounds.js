import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import i18n from "../../i18n";
import { Box, CircularProgress } from '@mui/material';
import { DivisionResults } from './DivisionResults';

export class Rounds extends Component {
  static displayName = Rounds.name;

  constructor(props) {
    super(props);
    this.state = { rounds: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderRoundsTable(rounds) {
    return (<div>           
      <Collapse accordion={false}>
        {this.getItems(rounds)}
      </Collapse>
    </div>
    );
  }

  getItems(rounds) {
    const items = [];
    for (let i = 0, len = rounds.length; i < len; i++) {      
      items.push(
        <Panel header={`${rounds[i].eventName}`} key={rounds[i].eventId}>
          <DivisionResults result={rounds[i].divisions} />
        </Panel>
      );
    }
    return items;
  }

  render() {
    let contents = this.state.loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : this.renderRoundsTable(this.state.rounds);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('rounds_header')}</h1>
        <p>{i18n.t('rounds_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('api/series/event-results');
    const data = await response.json();
    this.setState({ rounds: data, loading: false });
  }
}

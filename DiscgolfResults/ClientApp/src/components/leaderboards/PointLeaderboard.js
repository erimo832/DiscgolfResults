import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";
import { Grid } from '../common/Grid';

export class PointLeaderboard extends Component {
  static displayName = PointLeaderboard.name;
  
  constructor(props) {
    super(props);
    this.state = 
    { 
      series: [], 
      loading: true,
      selectedPlayers: [],
      sortColumn: 'place',
      sortOrder: 'desc'
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderSeriesTable(series) {
    return (<div>
      <Collapse
        accordion={false}>
        {this.getItems(series)}
      </Collapse>
    </div>
    );
  }

  getItems(series) {
    const items = [];
    
    for (let i = 0, len = series.length; i < len; i++) {
      const key = i + 1;
      items.push(
        <Panel header={`${series[i].serieName}`} key={key}>
          <Container>
            <Row>
               <Col sm={12} lg={12}>
                   <div>{i18n.t('series_basednumrounds', {cnt: series[i].roundsToCount } )}</div>
               </Col>
            </Row>
            <Row>
              <Col sm={12} lg={12}>                
                <Grid data={this.getDataForGrid(series[i].placements)} format={this.getGridConf()} />
              </Col>
            </Row>
        </Container>
        </Panel>
      );
    }
    return items;
  }

  getGridConf()
  {
    return {
      className: "table",
      key: "fullName",
      detailsArray: "topResults",
      detailsValue: "points",
      columns: [
        {columnName: "place",           headerText: i18n.t('column_place'),         headerClassName: "", rowClassName: ""},
        {columnName: "fullName",        headerText: i18n.t('column_name'),          headerClassName: "", rowClassName: ""},
        {columnName: "totalPoints",     headerText: i18n.t('column_totalpoints'),   headerClassName: "", rowClassName: ""},
        {columnName: "avgPoints",       headerText: i18n.t('column_avgpoints'),     headerClassName: "d-none d-lg-table-cell", rowClassName: "d-none d-lg-table-cell"},
        {columnName: "avgHcpScore",     headerText: i18n.t('column_avghcpscore'),   headerClassName: "d-none d-md-table-cell", rowClassName: "d-none d-md-table-cell"},
        {columnName: "totalHcpScore",   headerText: i18n.t('column_totalhcpscore'), headerClassName: "d-none d-lg-table-cell", rowClassName: "d-none d-lg-table-cell"},
        {columnName: "maxPoints",       headerText: i18n.t('column_maxpoints'),     headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},
        {columnName: "minPoints",       headerText: i18n.t('column_minpoints'),     headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},        
        {columnName: "numberOfRounds",  headerText: i18n.t('column_rounds'),        headerClassName: "d-none d-lg-table-cell", rowClassName: "d-none d-lg-table-cell"}        
      ]
    };
  }

  getDataForGrid(data)
  {
    data.forEach(x => 
    { // add aggragated values
      x.maxPoints = x.topResults[0].points;
      x.minPoints = x.topResults[x.topResults.length - 1].points
    });

    return data;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : this.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_points_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch(process.env.REACT_APP_API_ENDPOINT +'/api/Series/hcpLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}

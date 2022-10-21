import { useTranslation } from 'react-i18next';
import { Grid } from '../common/Grid';

export function Results(props) {
    const { i18n } = useTranslation();

    function getGridConf()
    {
      return {
        className: "table table-striped",
        key: "fullName",
        detailsArray: "",
        detailsValue: "",
        columns: [
          {columnName: "placementHcp",  headerText: i18n.t('column_place'),     headerClassName: "", columnClass: ""},
          {columnName: "fullName",      headerText: i18n.t('column_name'),      headerClassName: "", columnClass: ""},
          {columnName: "points",        headerText: i18n.t('column_points'),    headerClassName: "", columnClass: ""},
          {columnName: "score",         headerText: i18n.t('column_score'),     headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
          {columnName: "hcpBefore",     headerText: i18n.t('column_hcp'),       headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
          {columnName: "hcpAfter",      headerText: i18n.t('column_hcpafter'),  headerClassName: "d-none d-md-table-cell", columnClass: "d-none d-md-table-cell"},
          {columnName: "hcpScore",      headerText: i18n.t('column_hcpscore'),  headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
          {columnName: "numberOfCtps",  headerText: i18n.t('column_ctp'),       headerClassName: "d-none d-md-table-cell", columnClass: "d-none d-md-table-cell"}        
        ]
      };
    }
  
    function getDataForGrid(data)
    {
      data.forEach(x => 
        { // add aggragated values
          x.isCtp = x.ctp === true ? "1": "0";        
        });
  
      return data;
    }

    return (
        <>            
            <Grid data={getDataForGrid(props.results)} format={getGridConf()} />
        </>
    ); 
}
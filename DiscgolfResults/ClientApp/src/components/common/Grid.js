import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';

export class Grid extends Component {
  constructor(props) {    
    super(props);

    this.state = 
    { 
      data: props.data,
      selectedRows: [],
      format: props.format,
      sortColumn: 'place',
      sortOrder: 'desc'
    };
  }

  handleRowSelected(name) {
    let rows = this.state.selectedRows;
    if(rows.includes(name))
        rows.splice(rows.indexOf(name), 1);//removes selection
    else
        rows.push(name);
    
    this.setState({ selectedRows: rows });
  }

  
  handleOnSort(col)
  {
    let prevSortCol = this.state.sortColumn;
    let prevSortOrder = this.state.sortOrder;
    let nextSortCol = col;
    let nextSortOrder = 'desc';

    if(col === prevSortCol) {
      nextSortOrder = prevSortOrder === 'desc' ? 'asc' : 'desc';
    }

    let data = this.state.data.sort(this.compare(nextSortCol, nextSortOrder));

    this.setState({ sortColumn: nextSortCol });
    this.setState({ sortOrder: nextSortOrder });
    this.setState({ data: data });
  }

  //Refactor move own component
  compare = (col, order) => (a, b) =>
  {
    if(typeof a[col] === 'string')
    {
      if (a[col].toLowerCase() > b[col].toLowerCase()) return order === 'desc' ? 1 : -1;
      if (b[col].toLowerCase() > a[col].toLowerCase()) return order === 'desc' ? -1 : 1;
    }
    else 
    {
      if (a[col] > b[col]) return order === 'desc' ? 1 : -1;
      if (b[col] > a[col]) return order === 'desc' ? -1 : 1;
    }
    return 0;
  }

  getHeader()
  {
    return (
        <thead>
            <tr key="header">
                {this.state.format.columns.map(x => <th key={"header_" + x.columnName} className={x.headerClassName} onClick={() => this.handleOnSort(x.columnName)}>{x.headerText}</th>) }
            </tr>
        </thead>
    );
  }

  getRow(obj)
  {
    if(obj.isDetails)
    {
        return (
        <tr key={obj.key}>
            <td colSpan={this.state.format.columns.length} style={{textAlign: 'center'}}>{obj.value}</td>
        </tr>
        );
    }

    return (
        <tr key={obj.key} onClick={() => this.handleRowSelected(obj.key)} >
            {this.state.format.columns.map(x => <td key={obj.key + x.columnName} className={x.rowClassName}>{obj.value[x.columnName]}</td> )}
        </tr>
        );
  }

  getDataWithDetails(data)
  {
    let result = [];

    data.forEach(x =>
      {
        result.push(
        {
          isDetails: false,
          key: x[this.state.format.key],
          value: x
        });

        if(this.state.format.detailsValue !== "" && this.state.selectedRows.includes(x[this.state.format.key]))
        {
            let col = this.state.format.detailsValue;
            result.push(
            {
              isDetails: true,
              key: x[this.state.format.key] + "_details",
              value: x[this.state.format.detailsArray].map(y => y[col]).join(' - ')
            });
        }
      }
    );
    
    return result;
  }

  render() {    
    let obj = this.getDataWithDetails(this.state.data);

    const content = (
        <table className={this.state.format.className} aria-labelledby="tabelLabel">{/* table-striped */}
            {this.getHeader()}
            <tbody>
                { obj.map(obj =>
                    this.getRow(obj)
                )}
            </tbody>
        </table>
    );

    return content;
  }
}

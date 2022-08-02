import { Box, CircularProgress, TextField } from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { NavLink } from 'reactstrap';

export function Courses(props) {
  const { i18n } = useTranslation();
  const [state, setState] = useState( {
    loading: true,
    courses: null,
    filtered: null
  }); 

  useEffect(() => {
    if(state.courses === null)
      fetchData(true);
  });

  let contents = state.loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : renderTable(state.filtered);

  const handleChange = (event) => {
    let filtered = state.courses.filter(x => x.name.toLowerCase().includes(event.target.value.toLowerCase()));
    
    setState({...state,
          filtered: filtered.map(x => x)
    });
  };

  function renderTable(courses) {
    return (
      <>
        <TextField id="outlined-basic" fullWidth label={i18n.t('common_filter_course')} variant="outlined" onChange={ (e) => handleChange(e) } />
        <table className='table table-condensed table-striped table-sm' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>{i18n.t('column_name')}</th>
            </tr>
          </thead>
          <tbody>
            {courses.map(x =>
              <tr key={x.courseId}>
                  <td>                  
                    <NavLink href={"courses/" + x.courseId } className="text-dark">{x.name}</NavLink>
                  </td>
              </tr>
            )}
          </tbody>
        </table>
      </>
    );
  }
  async function fetchData() {
    const response = await fetch('/api/courses');
    const data = await response.json();
    setState({
      loading: false,
      courses: data,
      filtered: data.map(x => x)
    })
  }

  return (
    <div>
      <h1 id="tabelLabel">{i18n.t('courses_header')}</h1>
      <p>{i18n.t('courses_description')}</p>      
      {contents}
    </div>
  );  
}
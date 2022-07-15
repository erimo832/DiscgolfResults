import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, UncontrolledDropdown, DropdownMenu, DropdownItem, DropdownToggle } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import i18n from '../i18n';

export class NavMenu extends Component {
  //static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  changeLanguage(lng) {
    i18n.changeLanguage(lng);
    this.setState({
      lang: true
    });
  }

  render () { 
    /*
    <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/players">nt_Players</NavLink>
                </NavItem>
    */
    return (
      <header>        
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand href={process.env.REACT_APP_NAVBAR_BaseLink}>{process.env.REACT_APP_NAVBAR_BASELINKTEXT}</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">{i18n.t('menu_home')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/hcp">{i18n.t('menu_hcp')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/players">{i18n.t('menu_players')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/rounds">{i18n.t('menu_rounds')}</NavLink>
                </NavItem>
                
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle className="text-dark" nav caret>
                  {i18n.t('menu_leaderboards')}
                  </DropdownToggle>
                  <DropdownMenu end>
                    <DropdownItem>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/leaderboards/point">{i18n.t('menu_pointleaderboard')}</NavLink>
                      </NavItem>               
                    </DropdownItem>
                    <DropdownItem>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/leaderboards/score">{i18n.t('menu_scoreleaderboard')}</NavLink>
                      </NavItem>
                    </DropdownItem>
                    <DropdownItem>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/leaderboards/ctp">{i18n.t('menu_ctpleaderboard')}</NavLink>
                      </NavItem>
                    </DropdownItem>  
                  </DropdownMenu>
                </UncontrolledDropdown>
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle className="text-dark" nav caret>
                    {i18n.t('language')}
                  </DropdownToggle>
                  <DropdownMenu end>
                    <DropdownItem>
                      <NavItem onClick={() => this.changeLanguage('sv')}>
                        <NavLink tag={Link} className="text-dark" to="/">Svenska</NavLink>
                      </NavItem>
                    </DropdownItem>
                    <DropdownItem  onClick={() => this.changeLanguage('en')}>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/">English</NavLink>
                      </NavItem>
                    </DropdownItem>                    
                  </DropdownMenu>
                </UncontrolledDropdown>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

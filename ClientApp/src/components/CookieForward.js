import React, { Component } from 'react';

export class CookieForward extends Component {
  static displayName = CookieForward.name;

  constructor(props) {
    super(props);
    this.state = { response: null, loading: true, success: false };
  }

  componentDidMount() {
    this.populateBin();
  }

  render() {
    const results = httpBin => (
      <table>
        <tr>
          <td>
            <strong>Origin:&nbsp;</strong>
          </td>
          <td>{httpBin.origin}</td>
        </tr>
        <tr>
          <td>
            <strong>URL:&nbsp;</strong>
          </td>
          <td>{httpBin.url}</td>
        </tr>
        <tr>
          <td>
            <strong>Cookies:&nbsp;</strong>
          </td>
          <td>{httpBin.headers.cookie ?? 'NONE'} </td>
        </tr>
      </table>
    );

    const display = (
      <>
        <h1>
          Cookie Forward Results:&nbsp;
          {this.state.loading
            ? 'Loading...'
            : this.state.success
            ? 'Succes'
            : 'Failed'}
        </h1>
        {this.state.loading || !this.state.success ? (
          <></>
        ) : (
          results(this.state.response)
        )}
      </>
    );

    return display;
  }

  async populateBin() {
    fetch('cookieforwarding')
      .then(response => response.json())
      .then(httpBin =>
        this.setState(previous => ({
          ...previous,
          response: httpBin,
          loading: false,
          success: true,
        })),
      )
      .catch(error => {
        console.error(error);
        this.setSete(previous => ({
          ...previous,
          loading: false,
          success: false,
        }));
      });
  }
}

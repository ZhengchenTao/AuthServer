import styled from 'styled-components';

// tslint:disable-next-line: variable-name
export const LoadingStyleWrap = styled.div`
  .loading {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    &-container {
      padding: 100px 0;
      text-align: center;
    }

    $-circle {
      box-sizing: border-box;
      border-radius: 50%;
      border-width: 1.5px;
      border-style: solid;
      border-color: rgb(0, 120, 212) rgb(199, 224, 244) rgb(199, 224, 244);
      border-image: initial;
      width: 30px;
      height: 30px;
      animation-name: spinner;
      animation-duration: 1.3s;
      animation-iteration-count: infinite;
      animation-timing-function: cubic-bezier(0.53, 0.21, 0.29, 0.67);
    }
  }

  @keyframes spinner {
    from {
      transform: rotate(0deg);
    }

    to {
      transform: rotate(360deg);
    }
  }
`;

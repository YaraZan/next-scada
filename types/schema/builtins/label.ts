import type { NodeProperties } from '../properties';

export interface Label extends Node {
  properties?: LabelProperties | null
}

type LabelProperties = {
  fontSize: {
    measurement: 'px',
    value: 14,
  },
  fontWeight: {
    value: 500,
  },
  textContent: {
    value: 'Text label',
  },
} & NodeProperties;

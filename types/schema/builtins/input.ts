import type { NodeProperties } from '../properties';
import type { SchemaNode } from '../node';

export interface Input extends SchemaNode {
  properties?: InputProperties | null
}

type InputProperties = {
  textContent: {
    value: null,
  },
  placeholder: {
    value: 'Placeholder',
  },
} & NodeProperties;

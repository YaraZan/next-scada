import type { NodeProperties } from '../properties';

export interface Image extends Node {
  properties?: ImageProperties | null
}

type ImageProperties = {
  backgroundImage: {
    value: null,
  },
} & NodeProperties;

export * from './utils';

// NOTE: there is no syntax for: export {* as bits} from './lib/bits';
import * as bits_ from './bits';
/**
 * Export module bits.
 */
export let bits = bits_;

import vec2 from './vec2';
import vec3 from './vec3';
import vec4 from './vec4';
import quat from './quat';
import mat2 from './mat2';
import mat23 from './mat23';
import mat3 from './mat3';
import mat4 from './mat4';
import color3 from './color3';
import color4 from './color4';

export { vec2, vec3, vec4, quat, mat2, mat23, mat3, mat4, color3, color4 };
export default { vec2, vec3, vec4, quat, mat2, mat23, mat3, mat4, color3, color4 };

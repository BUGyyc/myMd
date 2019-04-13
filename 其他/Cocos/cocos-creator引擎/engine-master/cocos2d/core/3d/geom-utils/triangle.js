const vec3 = cc.vmath.vec3;

/**
 * @class geomUtils.Triangle
 * @param {Number} ax 
 * @param {Number} ay 
 * @param {Number} az 
 * @param {Number} bx 
 * @param {Number} by 
 * @param {Number} bz 
 * @param {Number} cx 
 * @param {Number} cy 
 * @param {Number} cz 
 */
function triangle(ax, ay, az, bx, by, bz, cx, cy, cz) {
    this.a = vec3.create(ax, ay, az);
    this.b = vec3.create(bx, by, bz);
    this.c = vec3.create(cx, cy, cz);
}

/**
 * create a new triangle
 * @method create
 * @param {number} ax
 * @param {number} ay
 * @param {number} az
 * @param {number} bx
 * @param {number} by
 * @param {number} bz
 * @param {number} cx
 * @param {number} cy
 * @param {number} cz
 * @return {geomUtils.Triangle}
 */
triangle.create = function (ax, ay, az, bx, by, bz, cx, cy, cz) {
    return new triangle(ax, ay, az, bx, by, bz, cx, cy, cz);
}

/**
 * clone a new triangle
 * @method clone
 * @param {geomUtils.Triangle} t the source plane
 * @return {geomUtils.Triangle}
 */
triangle.clone = function (t) {
    return new triangle(
        t.a.x, t.a.y, t.a.z,
        t.b.x, t.b.y, t.b.z,
        t.c.x, t.c.y, t.c.z
    );
}

/**
 * copy the values from one triangle to another
 * @method copy
 * @param {geomUtils.Triangle} out the receiving triangle
 * @param {geomUtils.Triangle} t the source triangle
 * @return {geomUtils.Triangle}
 */
triangle.copy = function (out, t) {
    vec3.copy(out.a, t.a);
    vec3.copy(out.b, t.b);
    vec3.copy(out.c, t.c);

    return out;
}

/**
 * Create a triangle from three points
 * @method fromPoints
 * @param {geomUtils.Triangle} out the receiving triangle
 * @param {vec3} a
 * @param {vec3} b
 * @param {vec3} c
 * @return {geomUtils.Triangle}
 */
triangle.fromPoints = function (out, a, b, c) {
    vec3.copy(out.a, a);
    vec3.copy(out.b, b);
    vec3.copy(out.c, c);
    return out;
}

/**
 * Set the components of a triangle to the given values
 *
 * @method set
 * @param {geomUtils.Triangle} out the receiving plane
 * @param {number} ax X component of a
 * @param {number} ay Y component of a
 * @param {number} az Z component of a
 * @param {number} bx X component of b
 * @param {number} by Y component of b
 * @param {number} bz Z component of b
 * @param {number} cx X component of c
 * @param {number} cy Y component of c
 * @param {number} cz Z component of c
 * @return {plane}
 */
triangle.set = function (out, ax, ay, az, bx, by, bz, cx, cy, cz) {
    out.a.x = ax;
    out.a.y = ay;
    out.a.z = az;

    out.b.x = bx;
    out.b.y = by;
    out.b.z = bz;

    out.c.x = cx;
    out.c.y = cy;
    out.c.z = cz;

    return out;
}

module.exports = triangle;

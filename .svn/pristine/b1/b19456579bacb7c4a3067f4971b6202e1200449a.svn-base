using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Maths
{
    class Vector3
    {
        public float x;
        public float y;
        public float z;

        /**
         *  default constructor
         */
        public Vector3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        /**
         *  constructor with values for x,y and z
         */
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /**
         *  method to invert the vector
         */
        public void Invert()
        {
            this.x = -x;
            this.y = -y;
            this.z = -z;
        }

        /**
         *  returns magnitude of the vector
         */
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        /**
         *  returns the square of the magnitude
         *  helps to compare magnitudes without doing complex sqrt 
         */
        public float SquareMagnitude()
        {
            return x * x + y * y + z * z;
        }

        /**
         *  normalises the vector using the magnitude
         */
        public void Normalise()
        {
            float magnitude = Magnitude();
            if (magnitude > 0)
            {
                this.x = this.x / magnitude;
                this.y = this.y / magnitude;
                this.z = this.z / magnitude;
            }
        }

        public Vector3 ReturnNormalise()
        {
            Vector3 retVector = this;
            retVector.Normalise();
            return retVector;
        }

        /**
         *  multiple the vector by a scalar value
         */
        public void MultipleByScalar(float scalar)
        {
            if (scalar != 0.0f)
            {
                this.x *= scalar;
                this.y *= scalar;
                this.z *= scalar;
            }
        }

        /**
         *  returns a copy of this. multiplied by the scalar
         */
        public Vector3 ReturnMultipleByScalar(float scalar)
        {
            if (scalar != 0.0f)
                return new Vector3(this.x * scalar, this.y * scalar, this.z * scalar);
            else
                return this;
        }

        /**
         *  adds a vector3 to this. 
         */
        public void AddVector(Vector3 vector)
        {
            this.x += vector.x;
            this.y += vector.y;
            this.z += vector.z;
        }

        /**
         *  returns the value vector3 added to this.
         */
        public Vector3 ReturnAddVector(Vector3 vector)
        {
            return new Vector3(this.x + vector.x, this.y + vector.y, this.z + vector.z);
        }
        
        /**
         *  subtracts vector3 from this.
         */
        public void SubtractVector(Vector3 vector)
        {
            this.x -= vector.x;
            this.y -= vector.y;
            this.z -= vector.z;
        }

        /**
         *  returns value vector3 subtracted from this.
         */
        public Vector3 ReturnSubtractVector(Vector3 vector)
        {
            return new Vector3(this.x - vector.x, this.y - vector.y, this.z - vector.z);
        }

        /**
         *  scales vector3 and adds to this.
         */
        public void AddScalarVector(Vector3 vector, float scalar)
        {
            if (scalar != 0.0f && vector!= new Vector3())
            {
                this.x += vector.x * scalar;
                this.y += vector.y * scalar;
                this.z += vector.z * scalar;
            }
            else
            {
                this.x += vector.x;
                this.y += vector.y;
                this.z += vector.z;
            }
        }

        public Vector3 ReturnAddScalarVector(Vector3 vector, float scalar)
        {
            Vector3 result = this;
            if (scalar != 0.0f && vector != new Vector3())
            {
                result.x += vector.x * scalar;
                result.y += vector.y * scalar;
                result.z += vector.z * scalar;
            }
            else
            {
                result.x += vector.x;
                result.y += vector.y;
                result.z += vector.z;
            }
            return result;
        }
        public Vector3 ReturnAbsoluteVector()
        {
            Vector3 result = new Vector3();
            result.x = this.x < 0 ? this.x * -1 : this.x;
            result.y = this.y < 0 ? this.y * -1 : this.y;
            result.z = this.z < 0 ? this.z * -1 : this.z;
            return result;
        }
        public void AbsoluteVector()
        {
            this.x = this.x < 0 ? this.x * -1 : this.x;
            this.y = this.y < 0 ? this.y * -1 : this.y;
            this.z = this.z < 0 ? this.z * -1 : this.z;
        }
        /**
         *  Returns ComponentProduct of this. by vector3
         */
        public Vector3 ReturnComponentProduct(Vector3 vector)
        {
            return new Vector3(this.x * vector.x, this.y * vector.y, this.z * vector.z);
        }

        /**
         *  finds ComponentProduct of this. by vector3 
         */
        public void ComponentProduct(Vector3 vector)
        {
            this.x *= vector.x;
            this.y *= vector.y;
            this.z *= vector.z;
        }

        /**
         *  returns a float the dotproduct of this. by vector3
         */
        public float ReturnDotProduct(Vector3 vector)
        {
            return this.x * vector.x + this.y * vector.y + this.z * vector.z;
        }

        public float ReturnAngle(Vector3 vector)
        {
            Vector3 thisNormal = this.ReturnNormalise();
            float dotProduct = thisNormal.ReturnDotProduct(vector.ReturnNormalise());

            return (float)(Math.Acos((double)dotProduct) * (180.0/Math.PI));
        }

        /**
         *  returns a vector3 of cross product of this. and vector3 
         *  not needed for 2d but still here in case
         */
        public Vector3 ReturnCrossProduct(Vector3 vector)
        {
            return new Vector3(this.y * vector.z - this.z * vector.y,
                               this.z * vector.x - this.x * vector.z,
                               this.x * vector.y - this.y * vector.x);
        }

        public Boolean ReturnAllGreaterAndEqualThen(Vector3 vector)
        {
            if (this.x >= vector.x &&
                this.y >= vector.y &&
                this.z >= vector.z)
            {
                return true;
            }
            else 
                return false;
        }
        public Boolean ReturnAllLessAndEqualThen(Vector3 vector)
        {
            if (this.x <= vector.x &&
                this.y <= vector.y &&
                this.z <= vector.z)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean ReturnAnyGreaterThen(Vector3 vector)
        {
            if (this.x > vector.x ||
                this.y > vector.y ||
                this.z > vector.z)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean ReturnAnyLessThen(Vector3 vector)
        {
            if (this.x < vector.x ||
                this.y < vector.y ||
                this.z < vector.z)
            {
                return true;
            }
            else
                return false;
        }
        /**
         * sets this to cross product of this. by vector3
         *  not needed for 2d but still here in case
         */
        public void CrossProduct(Vector3 vector)
        {
            Vector3 value = ReturnCrossProduct(vector);
            this.x = value.x;
            this.y = value.y;
            this.z = value.z;
        }
    }
}
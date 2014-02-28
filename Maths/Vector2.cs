using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Maths
{
    class Vector2
    {
        public float x;
        public float y;

        /**
         *  default constructor
         */
        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        /**
         *  constructor with values for x and y
         */
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /**
         *  method to invert the vector
         */
        public void Invert()
        {
            this.x = -x;
            this.y = -y;
        }

        /**
         *  returns magnitude of the vector
         */
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        /**
         *  returns the square of the magnitude
         *  helps to compare magnitudes without doing complex sqrt 
         */
        public float SquareMagnitude()
        {
            return x * x + y * y;
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
            }
        }

        public Vector2 ReturnNormalise()
        {
            Vector2 retVector = this;
            retVector.Normalise();
            return retVector;
        }

        /**
         *  multiple the vector by a scalar value
         */
        public void MultipleByScalar(float value)
        {
            this.x *= value;
            this.y *= value;
        }

        /**
         *  returns a copy of this. multiplied by the scalar
         */
        public Vector2 ReturnMultipleByScalar(float value)
        {
            return new Vector2(this.x * value, this.y * value);
        }

        /**
         *  adds a vector2 to this. 
         */
        public void AddVector(Vector2 vector)
        {
            this.x += vector.x;
            this.y += vector.y;
        }

        /**
         *  returns the value vector2 added to this.
         */
        public Vector2 ReturnAddVector(Vector2 vector)
        {
            return new Vector2(this.x + vector.x, this.y + vector.y);
        }
        
        /**
         *  subtracts vector2 from this.
         */
        public void SubtractVector(Vector2 vector)
        {
            this.x -= vector.x;
            this.y -= vector.y;
        }

        /**
         *  returns value vector2 subtracted from this.
         */
        public Vector2 ReturnSubtractVector(Vector2 vector)
        {
            return new Vector2(this.x - vector.x, this.y - vector.y);
        }

        /**
         *  scales vector2 and adds to this.
         */
        public void AddScalarVector(Vector2 vector, float scale)
        {
            this.x += vector.x * scale;
            this.y += vector.y * scale;
        }

        /**
         *  Returns ComponentProduct of this. by vector2 :not very useful might be deleted
         */
        public Vector2 ReturnComponentProduct(Vector2 vector)
        {
            return new Vector2(this.x * vector.x, this.y * vector.y);
        }

        /**
         *  finds ComponentProduct of this. by vector2 :not very useful might be deleted
         */
        public void ComponentProduct(Vector2 vector)
        {
            this.x *= vector.x;
            this.y *= vector.y;
        }

        /**
         *  returns a float the dotproduct of this. by vector2
         */
        public float ReturnDotProduct(Vector2 vector)
        {
            return this.x * vector.x + this.y * vector.y ;
        }

        public float ReturnAngle(Vector2 vector)
        {
            Vector2 thisNormal = this.ReturnNormalise();
            float dotProduct = thisNormal.ReturnDotProduct(vector.ReturnNormalise());

            return (float)(Math.Acos((double)dotProduct) * (180.0/Math.PI));
        }

        public Boolean ReturnAllGreaterThen(Vector2 vector)
        {
            if (this.x > vector.x &&
                this.y > vector.x)
            {
                return true;
            }
            else 
                return false;
        }
        public Boolean ReturnAllLessThen(Vector2 vector)
        {
            if (this.x < vector.x &&
                this.y < vector.x)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean ReturnAnyGreaterThen(Vector2 vector)
        {
            if (this.x > vector.x ||
                this.y > vector.x)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean ReturnAnyLessThen(Vector2 vector)
        {
            if (this.x < vector.x ||
                this.y < vector.x)
            {
                return true;
            }
            else
                return false;
        }
    }
}
